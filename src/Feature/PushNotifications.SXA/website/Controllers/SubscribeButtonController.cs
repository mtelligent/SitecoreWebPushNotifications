using SF.Feature.PushNotifications.SXA.Repositories;
using Sitecore.XA.Foundation.RenderingVariants.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SF.Feature.PushNotifications.SXA.Controllers
{
    public class SubscribeButtonController : VariantsController
    {
        protected readonly ISubscribeButtonRepository SubscribeButtonRepository;
        public SubscribeButtonController(ISubscribeButtonRepository subscribeButtonRepository)
        {
            SubscribeButtonRepository = subscribeButtonRepository;
        }

        protected override object GetModel()
        {
            return SubscribeButtonRepository.GetModel();
        }
    }
}