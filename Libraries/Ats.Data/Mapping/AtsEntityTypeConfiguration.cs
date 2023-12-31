﻿using System.Data.Entity.ModelConfiguration;

namespace TWYK.Data.Mapping
{
    public class AtsEntityTypeConfiguration<T> : EntityTypeConfiguration<T> where T : class
    {
        protected AtsEntityTypeConfiguration()
        {
            PostInitialize();
        }

        /// <summary>
        /// Developers can override this method in custom partial classes
        /// in order to add some custom initialization code to constructors
        /// </summary>
        protected virtual void PostInitialize()
        {

        }
    }
}