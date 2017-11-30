using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace SimpleLocalization.Xaml.Extensions
{
    [MarkupExtensionReturnType(typeof(object))]
    public abstract class BindingBaseExtension : MarkupExtension
    {
        protected BindingBaseExtension()
        {
        }

        protected BindingBaseExtension(PropertyPath path)
        {
            Path = path;
        }

        [ConstructorArgument("path")]
        public PropertyPath Path { get; set; }

        public IValueConverter Converter { get; set; }
        public object ConverterParameter { get; set; }
        public string ElementName { get; set; }
        public RelativeSource RelativeSource { get; set; }
        public object Source { get; set; }
        public bool ValidatesOnDataErrors { get; set; }
        public bool ValidatesOnExceptions { get; set; }
        public UpdateSourceTrigger UpdateSourceTrigger { get; set; } = UpdateSourceTrigger.PropertyChanged;
        [TypeConverter(typeof(CultureInfoIetfLanguageTagConverter))]
        public CultureInfo ConverterCulture { get; set; }
        public bool IsAsync { get; set; }
        public int Delay { get; set; }
        public string StringFormat { get; set; }
        public BindingMode Mode { get; set; } = BindingMode.OneWay;
        public object FallbackValue { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var pvt = serviceProvider?.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            if (pvt == null)
                return null;

            var targetObject = pvt.TargetObject as DependencyObject;
            if (targetObject == null)
                return null;

            var targetProperty = pvt.TargetProperty as DependencyProperty;
            if (targetProperty == null)
                return null;

            var binding = new Binding
            {
                Path = Path,
                Converter = Converter,
                ConverterCulture = ConverterCulture,
                ConverterParameter = ConverterParameter,
                ValidatesOnDataErrors = ValidatesOnDataErrors,
                ValidatesOnExceptions = ValidatesOnExceptions,
                Mode = Mode,
                UpdateSourceTrigger = UpdateSourceTrigger,
                IsAsync = IsAsync,
                Delay = Delay,
                StringFormat = StringFormat ?? "{0}"
            };

            if (ElementName != null)
                binding.ElementName = ElementName;
            if (RelativeSource != null)
                binding.RelativeSource = RelativeSource;
            if (Source != null)
                binding.Source = Source;
            if (FallbackValue != null)
                binding.FallbackValue = FallbackValue;

            PreBinding(targetObject, targetProperty, binding);

            var expression = BindingOperations.SetBinding(targetObject, targetProperty, binding);

            if (expression != null)
            {
                PostBinding(targetObject, targetProperty, binding, expression);
            }

            return targetObject.GetValue(targetProperty);
        }

        protected virtual void PostBinding(DependencyObject targetObject,
           DependencyProperty targetProperty, Binding binding,
           BindingExpressionBase expression)
        { }

        protected virtual void PreBinding(DependencyObject targetObject,
           DependencyProperty targetProperty, Binding binding)
        { }
    }
}
