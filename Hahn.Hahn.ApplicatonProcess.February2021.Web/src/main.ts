import {Aurelia} from 'aurelia-framework';
import * as environment from '../config/environment.json';
import {PLATFORM} from 'aurelia-pal';
import "../styles/styles.css";
import "font-awesome/css/font-awesome.css";
// import "bootstrap/dist/css/bootstrap.css";
//import "toastr/build/toastr.css"
import "../styles/toastr.css"
import "bootstrap-datepicker/dist/css/bootstrap-datepicker3.css"
import "../styles/bootstrap-flat.css";
import "../styles/bootstrap-flat-extras.css";
import "../styles/awesome-bootstrap-checkbox.css";
import "../styles/bootstrap.min.css";
import "../styles/bootstrap.min.js";
//import "../styles/font-awesome.css";

export function configure(aurelia: Aurelia): void {
  aurelia.use
    .standardConfiguration()
    //.feature(PLATFORM.moduleName('resources/index'))
    .plugin(PLATFORM.moduleName('aurelia-validation'))
    .plugin(PLATFORM.moduleName('aurelia-validatejs'))
    //.plugin("aurelia-validatejs");
    //.plugin('aurelia-datatable');
    //.feature(PLATFORM.moduleName('resources'));

  aurelia.use.developmentLogging(environment.debug ? 'debug' : 'warn');

  if (environment.testing) {
    aurelia.use.plugin(PLATFORM.moduleName('aurelia-testing'));
  }

  aurelia.start().then(() => aurelia.setRoot(PLATFORM.moduleName('asset-manager/asset')));
}
