import {Aurelia} from "aurelia-framework";
import {Router, RouterConfiguration, NavigationInstruction} from "aurelia-router";
import * as toastr from "toastr";
export class Index {
  router: Router;

  configureRouter(config: RouterConfiguration, router: Router) {
    config.title = "Asset Manager";
    config.map([
      { route: "", moduleId: "asset-manager/no-selection", nav: true, title: "Asset Manager",  name:"assetmanager"  },
    //  { route: "assets/:id", moduleId:"asset-manager/contact-detail", nave:true,  name:"contacts" }
    ]);

    this.router = router;
  }
}
