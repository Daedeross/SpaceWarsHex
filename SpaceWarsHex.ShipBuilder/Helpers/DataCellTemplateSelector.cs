using SpaceWarsHex.ShipBuilder.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SpaceWarsHex.ShipBuilder.Helpers
{
    public class DataCellTemplateSelector : DataTemplateSelector
    {
        public DataTemplate? StringTemplate { get; set; }
        public DataTemplate? FloatTemplate { get; set; }
        public DataTemplate? IntegerTemplate { get; set; }
        public DataTemplate? EnumTemplate { get; set; }
        public DataTemplate? CheckboxTemplate { get; set; }

        public override DataTemplate? SelectTemplate(object item, DependencyObject container)
        {
            if (item is PropertyViewModel vm)
            {
                return vm.Value switch
                {
                    string => StringTemplate,
                    double => FloatTemplate,
                    float => FloatTemplate,
                    int => IntegerTemplate,
                    bool => CheckboxTemplate,
                    Enum => EnumTemplate,
                    _ => null
                }; 
            }

            return null;
        }
    }
}
