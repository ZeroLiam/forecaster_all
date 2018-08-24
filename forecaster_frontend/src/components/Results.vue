/**
App     : Forecaster
File    : /components/Results.vue
Purpose : Renders results
Version : 1.0
Author  : Liselot Ramirez
Date    : 17.08.2018
**/
<template>
  <div id="results_component">
      <!-- Render the city found from the parent. City component gets today's data, therefore is weather[0] -->
       <City v-if="weather[0].city" :thecity="weather[0].city" :tempdata="weather[0]" />

      <!-- Iterates the weather prop and renders a card with each day from the forecast.
          Since weather[0] is today's weather, then we can iterate from 1 forwards to the 5 day forecast.
       -->
        <div class="results-container">
            <Card
                v-for="item in weather"
                v-if="item.id >= 1"
                :key="item.id"
                :tempdata="item"
            ></Card>
        </div> 
  </div>
</template>

<script>
// Let's import Card to pass the forecast result data, and City to pass today's weather and city name
import Card from "@/components/Card.vue";
import City from "@/components/City.vue";

export default {
  name: "Results",
  props: {
    weather: Array
  },
  components: {
    City,
    Card
  }
};
</script>
