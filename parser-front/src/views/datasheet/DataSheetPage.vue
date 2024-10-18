<template>
  <div class="container mt-4">
    <h2>Data Sheet Details</h2>

    <div v-if="datasheet">
      <h3>{{ datasheet.name }} Is {{datasheet.templateName}}</h3>

      <h4 class="mt-4">Values</h4>
      <ul class="list-group">
        <li v-for="value in datasheet.values" :key="value.id" class="list-group-item">
          <div class="row">
            <div class="col-md-4">
              <strong>{{ value.name }}:</strong>
            </div>
            <div class="col-md-4">
              <span v-if="value.type === 4 && !value.value">({{value.formula}})</span>
              <em v-else>{{ value.value }}</em>
            </div>
            <div class="col-md-4">
              <button v-show="value.type === 4" class="btn btn-primary" @click="calculateFormula(value.id)">Calculate</button>
            </div>
          </div>
        </li>
      </ul>

      <div class="mt-4">
        <button @click="editDataSheet" class="btn btn-success">Edit Data Sheet</button>
      </div>
    </div>

    <div v-else>
      <p>Loading data sheet...</p>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { useRoute } from 'vue-router';
import axios from 'axios';
import router from "@/router";

export default defineComponent({
  data() {
    return {
      sheetId: useRoute().params.id,

      datasheet: null as {
        id: number,
        name: string,
        templateName: string,
        values: {
          id: number,
          name: string,
          value: string,
          formula: string,
          type: number
        }[]
      } | null
    };
  },
  async created() {
    try {
      const response = await axios.get(`/api/datasheet/${this.sheetId}`);
      this.datasheet = response.data.data;
    } catch (error) {
      console.error('Error loading data sheet:', error);
    }
  },
  methods: {
    async editDataSheet() {
      await router.push({name: 'EditDataSheetPage', params: {id: this.sheetId}});
    },
    
    async calculateFormula(id: number) {
      try {
        const response = await axios.post(`/api/datasheet/calculate`, {DataSheetId: this.sheetId, ValueId: id});
        this.datasheet!.values.find((value) => value.id === id)!.value = response.data.data.result;
      } catch (error) {
        console.error('Error calculating formula:', error);
      }
    }
  }
});
</script>
