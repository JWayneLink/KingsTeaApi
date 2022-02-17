using KTA.Model.ExceptionHelper;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTA.Data.Entity
{
	[Serializable]
	public class BaseEntity
	{
        public bool ValidateEntity<T>(T entity)
        {
            var results = Validation.Validate(entity);

            if (!results.IsValid)
            {
                var sb = new StringBuilder();

                foreach (var result in results)
                {
                    sb.AppendLine(String.Format(CultureInfo.CurrentCulture, "{0}: {1}", result.Key, result.Message));
                }

                throw new DataValidateFailedException(sb.ToString());
            }

            return true;
        }
    }
}
