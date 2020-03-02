<template>
<div>
  <b-card header="Commission Data" header-tag="h4" v-if="commissionOutput!=null">
    <b-row>
      <b-col>
        <Chart :options="chartOptions"></Chart>
      </b-col>
      
      <b-col>
        <b-jumbotron
          header="Commission"
          lead="if the sales income below 80% of the target the commission will be 0">
          <h3 style="color:red;text-align: center;">{{commissionOutput.commission}}</h3>
        </b-jumbotron>
      </b-col>
    </b-row>
    <b-row>
      <b-col>
         <Chart :options="barChartOptions"></Chart>
      </b-col>
      <b-col>        <Chart :options="barChartOptions2"></Chart>
      </b-col>
    </b-row>
  </b-card>
  <customers-grid v-if="commissionOutput.newlySubscribedCustomers.length>0" :sourceData="commissionOutput.newlySubscribedCustomers" :title="'New Subsrcribed Customers'"></customers-grid>
  <customers-grid v-if="commissionOutput.oldSubscribedCustomers.length>0" :sourceData="commissionOutput.oldSubscribedCustomers" :title="'Old Subsrcribed Customers'"></customers-grid>

  </div>
</template>

<script>
import { Chart } from "highcharts-vue";
import customersGrid from "./CommissionCustomersGrid";
export default {
  components: { Chart,customersGrid },
  props:["commissionOutput"],
  watch: {
    commissionOutput: function (val) {
      if(val){
          this.chartOptions.series= [
          {
            name: "Share",
            data: [
              { name: "Sales Income", y: val.inputIncome },
              { name: "Rest Income",
               y: (val.totalRequiredIncome-val.inputIncome) }
            ]
          }
        ];

        this.barChartOptions.series=[
          {
            name: "Subscribed Customers",
            data: [val.totalCountCustomerSubscriped]
          },
          {
            name: "Called Customers",
            data: [val.totalCountCustomersCalled]
          }
        ];


         this.barChartOptions2.series=[
          {
            name: "New Subscribed Customers",
            color:"greenyellow",
            data: [val.newCustomerSubscripedCount]
          },
          {
            name: "Old Subscribed Customers",
            color:"teal",
            data: [val.oldCustomerSubscripedCount]
          }
        ];

      }
    }
  },
  data() {
    return {
      chartOptions: {
        chart: {
          plotBackgroundColor: null,
          plotBorderWidth: null,
          plotShadow: false,
          type: "pie"
        },
        title: {
          text: "Sales Income to Full Target Income"
        },
        tooltip: {
          pointFormat: "{series.name : } : <b>{point.percentage:.1f}%</b>"
        },
        plotOptions: {
          pie: {
            allowPointSelect: true,
            cursor: "pointer",
            dataLabels: {
              enabled: true,
              format: "<b>{point.name}</b>: {point.y}",
              connectorColor: "silver"
            }
          }
        },
        series: null
      },
      barChartOptions: {
        chart: {
          type: "column"
        },
        title: {
          text: "Customer Counts"
        },
         xAxis: {
        categories: ["Count"]},
        yAxis: {
          title: {
            text: "Customers Count"
          }
        },
        tooltip: {
          headerFormat: "<table>",
          pointFormat:
            '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
            '<td style="padding:0"><b>{point.y:.1f} Customer</b></td></tr>',
          footerFormat: "</table>",
          shared: true,
          useHTML: true
        },
        plotOptions: {
          column: {
            pointPadding: 0.2,
            borderWidth: 0
          }
        },
        series: null
      },
      barChartOptions2: {
        chart: {
          type: "column"
        },
        title: {
          text: "Subscribed Customer Counts"
        },
         xAxis: {
        categories: ["Count"]},
        yAxis: {
          title: {
            text: "Customers Count"
          }
        },
        tooltip: {
          headerFormat: "<table>",
          pointFormat:
            '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
            '<td style="padding:0"><b>{point.y:.1f} Customer</b></td></tr>',
          footerFormat: "</table>",
          shared: true,
          useHTML: true
        },
        plotOptions: {
          column: {
            pointPadding: 0.2,
            borderWidth: 0
          }
        },
        series: null
      },
    };
  }
};
</script>

<style>
</style>