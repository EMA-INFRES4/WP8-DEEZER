using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deezer_objects
{
    public static class Extensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> collection)
        {
            ObservableCollection<T> retour = new ObservableCollection<T>();
            foreach (T t in collection)
            {
                retour.Add(t);
            }
            return retour;
        }
    }
}
