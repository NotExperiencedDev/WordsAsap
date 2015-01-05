using System;
using System.Deployment.Application;
using System.Diagnostics;
using System.Reflection;

namespace WordsAsap
{
    public static class ApplicationInfo
    {      

        public static string FileVersionFunc()
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fvi.FileVersion;
        }

        public static string TitleFunc()
        {
            return ((AssemblyTitleAttribute)Attribute.GetCustomAttribute(
                                Assembly.GetEntryAssembly(), typeof(AssemblyTitleAttribute), false))
                               .Title;
        }

        public static string VersionFunc()
        {
            return
                        ApplicationDeployment.IsNetworkDeployed
                            ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
                            : Assembly.GetEntryAssembly().GetName().Version.ToString();
        }

        public static string ProductFunc()
        {
            return ((AssemblyProductAttribute)Attribute.GetCustomAttribute(
                               Assembly.GetEntryAssembly(), typeof(AssemblyProductAttribute), false))
                              .Product;
        }

        public static string CompanyFunc()
        {
            return ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(
                                Assembly.GetEntryAssembly(), typeof(AssemblyCompanyAttribute), false))
                               .Company;
        }
    }
}
