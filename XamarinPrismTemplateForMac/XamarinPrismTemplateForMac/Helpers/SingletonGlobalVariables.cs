using System;
namespace XamarinPrismTemplateForMac.Helpers
{
	public class SingletonGlobalVariables
	{
        public bool navigatedSavedNews  { get; set; }

        #region Singleton

        static SingletonGlobalVariables instance;

        public static SingletonGlobalVariables GetInstance() => instance ?? (instance = new SingletonGlobalVariables());

        #endregion

        public SingletonGlobalVariables()
		{
		}
	}
}

