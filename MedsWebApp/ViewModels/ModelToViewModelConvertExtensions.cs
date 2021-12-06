using System.Collections.Generic;
using System.Linq;

namespace MedsWebApp.ViewModels
{
    public interface IViewModel<TEntityViewModel>
    {
        public TEntityViewModel AsViewModel();
    }
    public static class ModelToViewModelConvertExtensions
    {
        public static IEnumerable<TEntityViewModel> AsViewModel<TEntityViewModel>(this IEnumerable<IViewModel<TEntityViewModel>> models) => models.Select(m => m.AsViewModel());
    }
}
