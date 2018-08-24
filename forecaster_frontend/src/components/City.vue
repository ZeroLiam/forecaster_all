/**
App     : Forecaster
File    : /components/City.vue
Purpose : Present the title of the city and today's weather conditions
Version : 1.0
Author  : Liselot Ramirez
Date    : 17.08.2018
**/
<template>
  <div id="city_result">
      <h1 id="city_title">{{thecity}}</h1>
      <!-- 
        thecity: Get's the city name
        tempdata.humidity: Shows humidity
        tempdata.windspeed: Shows windspeed
        tempdata.temp: Shows temperature degrees(Rounded number)
       -->
      <h2 id="city_data">Today in {{thecity}}: Humidity {{tempdata.humidity}}% <i class="fas fa-tint"></i> | Wind Speed {{tempdata.windspeed}} km/hr <i class="fas fa-feather"></i> | Temperature {{Math.round(tempdata.temp)}}&#176; </h2>
  </div>
</template>

<script>
export default {
  name: "City",
  props: {
    thecity: String,
    tempdata: Object
  }
};
</script>
