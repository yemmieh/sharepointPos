using Microsoft.SharePoint;
using POSManager.Domain.Logic.Abstract;
using POSManager.Domain.Models;
using POSManager.Domain.Service.Development;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace POSManager.Domain.Logic
{
    public class POSCache
    {
        SPWeb _web;
        public POSCache(SPWeb web)
        {
            _web = web;
        }

        public Settings POSSettings
        {
            get
            {
                string settingsKey = "POSManager.Settings";
                Settings posSettings = HttpRuntime.Cache[settingsKey] as Settings;
                if (posSettings == null)
                {
                    posSettings = GetPOSSettings(_web.Url);
                    HttpRuntime.Cache.Insert(settingsKey, posSettings, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(30));
                }
                return posSettings;
            }
        }

        private Settings GetPOSSettings(string url)
        {
            Settings settings = new Settings();
            Dictionary<string, string> valKey = new SettingsKey().GetKeys();
            POSMetaDataContext metaDb = new POSMetaDataContext(url);
            IQueryable<POSSettingsItem> settingsItem = metaDb.POSSettings.AsQueryable();
            string propertyValue = string.Empty;

            foreach (POSSettingsItem item in settingsItem)
            {
                if (!valKey.ContainsKey(item.Title))
                {
                    throw new ArgumentException(string.Format("Missing Setting in POS Manager Settings, Provide an Entry for {0}", item.Title));
                }
                propertyValue = valKey[item.Title];
                PropertyInfo pInfo = settings.GetType().GetProperty(propertyValue);
                if (pInfo == null)
                {
                    throw new ArgumentException(string.Format("No Matching property on Settings: {0}", item.Title));
                }
                pInfo.SetValue(settings, item.Value, null);

            }
            return settings;
        }

        public List<Branch> GetBranchList()
        {
            string BranchCacheKey = "POSManager.Branch";
            List<Branch> branches = HttpRuntime.Cache[BranchCacheKey] as List<Branch>;
            if (branches == null)
            {
                Settings appSettings = POSSettings;
                IBranch ibranch = (IBranch)Assembly.GetAssembly(typeof(IBranch)).CreateInstance((string.Format("{0}.{1}Branch", Constants.Namespace, appSettings.ApplicationMode)), false, BindingFlags.CreateInstance, null, null, null, null);
                ibranch.Settings = appSettings;
                branches = ibranch.GetBranches();
                //branches.Remove(branches.FirstOrDefault(c => c.Code == "001"));
                HttpRuntime.Cache.Insert(BranchCacheKey, branches, null, DateTime.Now.AddHours(1), TimeSpan.Zero);
            }
            return branches;
        }

        //public List<string> GetDepartment()
        //{
        //    string DepartmentCacheKey = "POSManager.Department";
        //    List<string> departments = HttpRuntime.Cache[DepartmentCacheKey] as List<string>;
        //    if (departments == null)
        //    {
        //        Settings appSettings = POSSettings;
        //        IDepartment idepartment = (IDepartment)Assembly.GetAssembly(typeof(IDepartment)).CreateInstance((string.Format("{0}.{1}Department", Constants.Namespace, appSettings.ApplicationMode)), false, BindingFlags.CreateInstance, null, null, null, null);
        //        string query = string.Empty;
        //        idepartment.Settings = appSettings;
        //        departments = idepartment.GetDepartments(query);
        //        //branches.Remove(branches.FirstOrDefault(c => c.Code == "001"));
        //        HttpRuntime.Cache.Insert(DepartmentCacheKey, departments, null, DateTime.Now.AddHours(1), TimeSpan.Zero);
        //    }
        //    return departments;
        //}

        private List<EmailTemplateItem> GetEmailTemplates()
        {
            string cacheKey = "POSManager.EmailTemplates";
            List<EmailTemplateItem> templates = HttpRuntime.Cache[cacheKey] as List<EmailTemplateItem>;
            if (templates == null)
            {
                POSMetaDataContext trackerMeta = new POSMetaDataContext(_web.Url);
                templates = trackerMeta.EmailTemplate.ToList();
                HttpRuntime.Cache.Insert(cacheKey, templates, null, DateTime.Now.AddHours(1), TimeSpan.Zero);
            }
            return templates;
        }

        public List<KeyValuePair<string, string>> GetState()
        {
            string cacheKey = "POSManager.StateListing";
            List<KeyValuePair<string, string>> states = HttpRuntime.Cache[cacheKey] as List<KeyValuePair<string, string>>;
            if (states == null)
            {
                IListing iListing = (IListing)Assembly.GetAssembly(typeof(IListing)).CreateInstance(string.Format("{0}.{1}Listing", Constants.Namespace, POSSettings.ApplicationMode), false, BindingFlags.CreateInstance, null, null, null, null);
                iListing.Settings = POSSettings;
                states = iListing.GetState("NG");
                HttpRuntime.Cache.Insert(cacheKey, states, null, DateTime.Now.AddYears(365), TimeSpan.Zero);
            }
            return states;
        }

        public List<string> GetLGA(string stateCode)
        {
            string cacheKey = string.Format("POSManager.LGAListing.{0}", stateCode);
            List<string> lgas = HttpRuntime.Cache[cacheKey] as List<string>;
            if (lgas == null)
            {
                IListing iListing = (IListing)Assembly.GetAssembly(typeof(IListing)).CreateInstance(string.Format("{0}.{1}Listing", Constants.Namespace, POSSettings.ApplicationMode), false, BindingFlags.CreateInstance, null, null, null, null);
                iListing.Settings = POSSettings;
                lgas = iListing.GetLGA(stateCode);
                HttpRuntime.Cache.Insert(cacheKey, lgas, null, DateTime.Now.AddYears(365), TimeSpan.Zero);
            }
            return lgas;
        }

        public List<string> GetDeparmentList()
        {
            string DepartmentCacheKey = "POSManager.Department";
            List<string> department = HttpRuntime.Cache[DepartmentCacheKey] as List<string>;
            if (department == null)
            {
                Settings appSettings = POSSettings;
                IDepartment idepartment = (IDepartment)Assembly.GetAssembly(typeof(IBranch)).CreateInstance((string.Format("{0}.{1}Department", Constants.Namespace, appSettings.ApplicationMode)), false, BindingFlags.CreateInstance, null, null, null, null);
                idepartment.Settings = appSettings;
                string query = "select * from vw_department where org_id = 1";
                department = idepartment.GetDepartments(query);
                HttpRuntime.Cache.Insert(DepartmentCacheKey, department, null, DateTime.Now.AddHours(1), TimeSpan.Zero);
            }
            return department;
        }

        private List<POSOptionsItem> GetPOSOptions()
        {
            string cacheKey = "POSManager.POSOptions";
            List<POSOptionsItem> templates = HttpRuntime.Cache[cacheKey] as List<POSOptionsItem>;
            if (templates == null)
            {
                POSMetaDataContext trackerMeta = new POSMetaDataContext(_web.Url);
                templates = trackerMeta.POSOptions.ToList();
                HttpRuntime.Cache.Insert(cacheKey, templates, null, DateTime.Now.AddHours(1), TimeSpan.Zero);
            }
            return templates;
        }

        public List<string> GetPOSOption(string option)
        {
            List<POSOptionsItem> templates = GetPOSOptions();
            POSOptionsItem template = templates.FirstOrDefault(x => x.Title == option);
            if (template == null)
            {
                throw new ArgumentException(string.Format("{0} Title does not exist in POS options list, add option and refresh", option));
            }
            return template.Values.Split('\n').ToList();
        }

        public EmailTemplateItem GetEmailTemplate(string title)
        {
            List<EmailTemplateItem> templates = GetEmailTemplates();
            EmailTemplateItem template = templates.FirstOrDefault(x => x.Title == title);
            if (template == null)
            {
                throw new ArgumentException(string.Format("{0} Title does not exist in Email Templates list, add template and refresh", title));
            }

            return template;
        }

        public SPUserCollection GetUsersInGroup(string groupName)
        {
            SPGroup group = _web.Groups[groupName];
            return group.Users;
        }
    }
}
