using System;
using Prism.Navigation;

namespace XamarinPrismTemplateForMac.Helpers
{
	public class SingletonGlobalVariables
	{
		public bool NavigatedFromSavedNewsOption { get; set; }

		#region Singleton

		private static SingletonGlobalVariables instance;

		public static SingletonGlobalVariables GetInstance() => instance ?? (instance = new SingletonGlobalVariables());

        #endregion

        public SingletonGlobalVariables()
		{
			instance = this;
		}
	}
}

