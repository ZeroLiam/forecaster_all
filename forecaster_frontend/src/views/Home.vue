/**
App     : Forecaster
File    : views/Home.vue
Purpose : Get data from backend and render into components
Version : 1.0
Author  : Liselot Ramirez
Date    : 17.08.2018
**/
<template>
  <div class="home">
    <!-- If someone wrote the city/zip code and clicked on search, the value is passed
        to this parent via the @clicked attribute. -->
    <Query @clicked="onSearchSubmit" />

    <!-- If your city/zip code was wrong and you found an error, show it! -->
    <div v-show="ajaxerror != null" class="ajaxerror">
      {{ajaxerror}}
    </div>

    <!-- If we have all the data and there's no error, then render the results -->
    <div v-if="allForecast.length > 0 && ajaxerror == null">
        <!-- :weather is the array with all the results objects -->
        <Results :weather="allForecast" />
    </div>
  </div>
</template>

<script>
// @ is an alias to /src
import Query from "@/components/Query.vue";
import Results from "@/components/Results.vue";

// Import axios for the ajax call (works with Vuejs)
// Import lodash to loop in the response and pass it to the result array
import axios from 'axios';
import _ from 'lodash';

export default {
  name: "home",
  components: {
    Query,
    Results
  },
  data: () => {
        return {
          reqCity: "your city",//request city (from search query component)
          allForecast: [],//all the result objects from the server's response 
          ajaxerror: null //in case there's an error in the ajax call, but don't fill this out yet
        }
  },
  methods: {
    onSearchSubmit (value) {
      this.reqCity = value; //the value we receive from the Query component via @clicked, to the reqCity variable
      this.getObj(); //After we get the value, then we can make the ajax call
      
    },
     getObj() {
       //url from the localhost; grabs forecast from the custom made ASP.Net Core API
      axios("https://localhost:5001/api/weather/forecast?city=" + this.reqCity,{
        method: 'GET',
        // THE FOLLOWING IS TO ALLOW THE LOCALHOST TO ACCESS THE BACKEND
        // I know this is a bad practice, but this is just for the localhost.
        // On production this will change, to reinforce credentials
        mode: 'no-cors',
        headers: {
            'Access-Control-Allow-Origin': '*',
            'Content-Type': 'application/json',
          }
      })
      // IF THE PROMISE SUCCEEDS...
      .then((response)  =>  {
        //We reset the allForecast variable, to clean it from previous value, enabling re-use
        this.allForecast.length = 0;
        
        //Let's find what we want: today's weather and forecast for the next 5 days
        //First, let's find the weather, and also assign an id. The response.data.forecast
        //comes with 40 items, starting with the weather for today at the last reading (from the 
        //OpenWeatherApp) until the equivalent last hour reading five days from now.
        //How can we find the five days forecast? Let's check it out:

        //VARS:
        // -----------------------------------------------------------------------------------
        // find: Keeps track of the index on the response.data.forecast. The data is read
        //       every three hours, so every 8 readings is the data of this same time period,
        //       but on the following days.
        // id:   We need to keep track of an id for today and the next days. This id will be
        //       passed on the result object we're collecting.
        // -----------------------------------------------------------------------------------
        var find = 0, id = 0;

        //Let's loop this baby
        for(var key = 0; key < response.data.forecast.length; key++){
          
          //Hey! If the key matches the find, then we got an object for the day!
          if(key === find){
            //Our object will contain id, city, day (as in date mm/dd), temperature, humidity, and wind speed
            //^^ (based on specs) 
            var obj = {
              id: id,
              city: response.data.city,
              day: response.data.forecast[find].dt_txt.split(" ")[0],
              temp: response.data.forecast[find].main.temp,
              humidity: response.data.forecast[find].main.humidity,
              windspeed: response.data.forecast[find].wind.speed            
            }
            //Once we capture our object, we push it to the allForecast array :)
            this.allForecast.push(obj);
            //Now find will be find + 7, i.e. second match will be key === 7 and find === 7
            find += 7;
            //Remember to update the id! Otherwise the objects will have the same id and that's
            //not good for our children components! (Do it for the children bro)
            id++;
          }
        }
        //After all the wilderness has passed, also put the ajaxerror as null here.
        //This avoids to have the ajaxerror displaying when the new query is successful.
        this.ajaxerror = null;
      })
      //IF THE PROMISE IS UNSUCCESSFUL...
      .catch((error) => {
        // Error! you found it? You post it!
        if(error.response.status == 400){
            this.ajaxerror = "That zip code or city is not in Germany, or is made up. Please review your input.";
        }
    });
    }
  }
};
</script>
