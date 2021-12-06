using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MedsWebApp.Models
{
    public abstract class BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool Active { get; set; } = true;

        public object Clone()
        {
            var type = GetType();
            var newObject = Activator.CreateInstance(type) as BaseModel;
            CopyFromTo(this, newObject);
            return newObject;
        }

        public void CopyFrom(BaseModel source)
        {
            CopyFromTo(source, this);
        }
        private static void CopyFromTo<TEntity>(TEntity source, TEntity destination) where TEntity : BaseModel
        {
            var sourceType = source.GetType();
            var destinationType = destination.GetType();
            if (sourceType != destinationType) throw new ArgumentException($"Can't copy data from source type: '${sourceType}' to destination type: '${destinationType}'");
            foreach (var property in sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!property.CanWrite) continue;
                property.SetValue(destination, property.GetValue(source));
            }
        }

        public override bool Equals(object obj)
        {
            var other = obj as BaseModel;
            if (other == null) return false;
            if (ReferenceEquals(other, this)) return true;
            return GetType() == other.GetType() && Id == other.Id && Id != -1;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(BaseModel obj1, BaseModel obj2)
        {
            if (obj1 is null && obj2 is null) return true;
            if (obj1 is null) return false;
            return obj1.Equals(obj2);
        }

        public static bool operator !=(BaseModel obj1, BaseModel obj2)
        {
            if (obj1 is null && obj2 is null) return false;
            if (obj1 is null) return true;
            return !obj1.Equals(obj2);
        }
    }
}
