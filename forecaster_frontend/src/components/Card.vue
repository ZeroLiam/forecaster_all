/**
App     : Forecaster
File    : /components/Card.vue
Purpose : Gets one object of weather's data, and shows it in a little card
Version : 1.0
Author  : Liselot Ramirez
Date    : 17.08.2018
**/
<template>
  <div :id="'card_component_' + tempdata.id" class="card-component">
      
      <div class="card-content">
            <div class="card-body">
                <!-- Shows the result data -->
                <div class="p-data">
                    <!-- 
                        tempdata.id: id of current object from parent
                        tempdata.day: Shows current day date
                     -->
                    <div class="card-day" :id="'day-'+tempdata.id">
                        {{tempdata.day.split("-")[1] + "/" + tempdata.day.split("-")[2]}}
                    </div>

                    <!-- 
                        tempdata.humidity: Shows humidity
                     -->
                    <div class="card-humidity" :id="'humidity-'+tempdata.id">
                        {{tempdata.humidity}}% <i class="fas fa-tint"></i>
                    </div>

                    <!-- 
                        tempdata.windspeed: Shows windspeed
                     -->
                    <div class="card-windspeed" :id="'windspeed-'+tempdata.id">
                        {{tempdata.windspeed}} km/hr <i class="fas fa-feather"></i>
                    </div>
                </div>
                    <!-- 
                        tempdata.temp: Shows temperature degrees(Rounded number)
                     -->
                <span class="p-degree" :id="'degree-' + tempdata.id">
                        {{Math.round(tempdata.temp)}}&#176;
                </span>
                
            </div>
        </div>
  </div>
</template>

<script>
export default {
  name: "Card",
  props: {
    tempdata: Object
  }
};
</script>
