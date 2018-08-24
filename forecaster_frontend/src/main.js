/**
App     : Forecaster
File    : main.js
Purpose : Main file
Version : 1.0
Author  : Liselot Ramirez
Date    : 17.08.2018
**/

import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import "./registerServiceWorker";
// Add the main styles to the entire app
import './styles/main-styles.scss';

Vue.config.productionTip = false;

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#app");
