import Vue from "vue";
import Router from "vue-router";
import Home from "./views/Home.vue";

Vue.use(Router);

export default new Router({
  mode: "history",
  base: process.env.BASE_URL,
  // We only have one route because we only 
  // have one task, one view
  routes: [
    {
      path: "/",
      name: "home",
      component: Home
    }
  ]
});
