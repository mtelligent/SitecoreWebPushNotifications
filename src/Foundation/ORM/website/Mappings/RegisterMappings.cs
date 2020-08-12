using Glass.Mapper.Sc.Pipelines.AddMaps;
using Toast.Foundation.ORM.Extensions;

namespace Toast.Foundation.ORM.Mappings
{
    public class RegisterMappings : AddMapsPipeline  {
        public void Process(AddMapsPipelineArgs args)
        {
            args.MapsConfigFactory.AddFluentMaps("Toast.Foundation.ORM");
        }
    }
}
