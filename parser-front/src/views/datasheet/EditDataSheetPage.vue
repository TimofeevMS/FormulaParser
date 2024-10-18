<template>
  <div class="container mt-4">
    <h2>Edit Data Sheet</h2>

    <div v-if="datasheet">
      <div class="mb-3">
        <input
            type="text"
            id="templateName"
            class="form-control"
            v-model="datasheet.name"
            placeholder="Enter data sheet name"
        />
      </div>

      <div>
        <h4>Values</h4>
        <div v-for="(value, index) in datasheet?.values" :key="value.id" class="mb-3 row">
          <div class="col-md-4">
            <strong>{{ value.name }}:</strong>
          </div>
          <div class="col-md-4">
            <input
                type="text"
                :id="'attr-' + index"
                class="form-control"
                v-model="value.value"
                :placeholder="value.type === 4 ? value.formula : 'Value'"
                :disabled="value.type === 4"
            />
          </div>
        </div>
  
        <div class="mt-5">
          <button @click="saveDataSheet" class="btn btn-success">Save Data Sheet</button>
        </div>
      </div>
    </div>

    <div v-else>
      <p>Loading data sheets...</p>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import axios from 'axios';
import router from "@/router";
import {useRoute} from "vue-router";

export default defineComponent({
  data() {
    return {
      sheetId: useRoute().params.id,
      
      datasheet: null as {
        templateId: number | null,
        name: string,
        values: {
          id: number,
          name: string,
          value: string,
          formula: string,
          type: number
        }[]
      } | null,
    };
  },
  async created() {
    try {
      const response = await axios.get(`/api/datasheet/${this.sheetId}`);
      this.datasheet = response.data.data;
    } catch (error) {
      console.error('Error fetching data sheet:', error);
      return null;
    }
  },
  
  methods: {
    validateDataSheet() {
      if (!this.datasheet?.name) {
        alert('Template name is required.');
        return false;
      }

      for (const value of this.datasheet?.values) {
        if (!value.value && value.type !== 4) {
          alert(`All fields for value are required.`);
          return false;
        }
      }
      return true;
    },

    async saveDataSheet() {
      if (!this.validateDataSheet()) {
        return;
      }

      try {
        const response = await axios.put(`/api/datasheet/${this.sheetId}`, this.datasheet);
        const sheetId = response.data.data;
        await router.push({ name: 'DataSheetPage', params: { id: sheetId } });
      } catch (error) {
        console.error('Error saving data sheet:', error);
      }
    },
  }
});
</script>
