using SimpleLocalization.Xaml.Extensions;
using System.Windows;
using System.Windows.Data;

namespace SimpleLocalization.Localization.Xaml
{
    public class LocExtension : BindingBaseExtension
    {
        protected override void PostBinding(
            DependencyObject targetObject,
            DependencyProperty targetProperty,
            Binding binding,
            BindingExpressionBase expression)
        {
            if (binding.Path != null)
            {
                targetObject.SetValue(targetProperty, string.Format(binding.StringFormat, Localizer.Current?[binding.Path.Path]));
            }
        }

        public LocExtension() : base()
        { }

        public LocExtension(PropertyPath path) : base(path)
        { }
    }
}
