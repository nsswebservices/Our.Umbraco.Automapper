using System.Collections.Generic;
using Our.Umbraco.Automapper.Framework.Attributes;
using uComponents.DataTypes.MultiUrlPicker.Dto;
using uComponents.DataTypes.UrlPicker.Dto;

namespace Our.Umbraco.Automapper.Framework.Mappers
{
    public class MapMultiUrlPickerPropertyMapper : AbstractHasPropertyAliasPropertyMapper<
                                                       MapFromMultiUrlPickerAttribute,
                                                       IEnumerable<UrlPickerState>,
                                                       IEnumerable<UrlPickerState>>
    {
        protected override IEnumerable<UrlPickerState> ConstructItem(string raw)
        {
            return MultiUrlPickerState.Deserialize(raw).Items;
        }
    }

    public class MapUrlPickerPropertyMapper : AbstractHasPropertyAliasPropertyMapper<
                                                  MapFromUrlPickerAttribute,
                                                  UrlPickerState,
                                                  UrlPickerState>
    {
        protected override UrlPickerState ConstructItem(string raw)
        {
            return UrlPickerState.Deserialize(raw);
        }
    }
}