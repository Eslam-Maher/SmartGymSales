<template>
  <b-card header="Search Customers" header-tag="h4">
    <b-row>
      <b-col>
        <b-form-group label="Name" label-for="Name-input">
          <b-form-input id="Name-input" v-model="searchCriteria.name" type="text" max="50"></b-form-input>
        </b-form-group>
      </b-col>
      <b-col>
        <b-form-group label="Mobile" label-for="Mobile-input">
          <b-form-input id="Mobile-input" v-model="searchCriteria.mobile" type="text" max="50"></b-form-input>
        </b-form-group>
      </b-col>
      <b-col>
        <b-form-group label="Email" label-for="Email-input">
          <b-form-input id="Email-input" v-model="searchCriteria.email" type="text" max="50"></b-form-input>
        </b-form-group>
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <b-form-group label="Source" label-for="Source-input">
          <multiselect
            id="Source-input"
            v-model="searchCriteria.source"
            :options="sourceOptions"
            label="name"
            track-by="id"
          ></multiselect>
        </b-form-group>
      </b-col>
      <b-col>
        <b-form-group label="Called Before" label-for="Called-input">
          <multiselect
            id="Called-input"
            v-model="searchCriteria.isCalled"
            :options="booleanOptions"
            label="name"
            track-by="id"
          ></multiselect>
        </b-form-group>
      </b-col>
      <b-col>
        <b-form-group label="Already Member" label-for="isSubscriped-input">
          <multiselect
            id="isSubscriped-input"
            v-model="searchCriteria.isSubscriped"
            :options="booleanOptions"
            label="name"
            track-by="id"
          ></multiselect>
        </b-form-group>
      </b-col>
    </b-row>
    <b-row>
          <b-col>
            <div class="button-group actions-buttons">
              <b-button @click="search" variant="primary">Search</b-button>
              <b-button @click="reset" variant="secondary">Reset</b-button>
            </div>
          </b-col>
        </b-row>
  </b-card>
</template>

<script>
export default {
data(){
    return {
        sourceOptions:[{id:1,name:"Sheet"},{id:2,name:"Sync"},{id:3,name:"Manual"}],
        booleanOptions:[{id:true,name:"Yes"},{id:false,name:"No"}],
        searchCriteria:{
            name:"",
            mobile:"",
            email:"",
            source:null,
            isCalled:null,
            isSubscriped:null
        }
    }
},
methods:{
    search:function(){
        this.$emit("refreshgrid",this.searchCriteria)
    },
    reset:function(){
        this.searchCriteria={
            name:"",
            mobile:"",
            email:"",
            source:null,
            isCalled:null,
            isSubscriped:null
        }
        this.$emit("refreshgrid",this.searchCriteria)
    }
}
}
</script>

<style>
</style>