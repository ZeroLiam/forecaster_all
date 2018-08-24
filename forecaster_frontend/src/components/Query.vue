/**
App     : Forecaster
File    : /components/Query.vue
Purpose : Get the zip code or city to look for
Version : 1.0
Author  : Liselot Ramirez
Date    : 17.08.2018
**/
<template>
  <div id="query_component">
    <p>Search for the forecast in your town </p>
      <input type="text" id="cityquery" name="cityquery" placeholder="City or zip code" />
      <button v-on:click="onSubmitQuery" class="search-btn" id="searchCity"><i class="fa fa-search fa-flip-horizontal"></i></button>
  </div>
</template>

<script>
// Let's get some jQuery in the house, just to speed up the writing
import JQuery from 'jquery';
let $ = JQuery;

export default {
  name: "Query",
  // No props this time. Maybe later?
  // props: {
    
  // },
  methods: {
    onSubmitQuery (evt) {
      //Once we click, please send the value to the parent. This way the parent can
      //do the ajax requests to the back end based on this result.
      let city = $("#cityquery").val();
      this.$emit("clicked", city);
    }
  }
};
</script>
