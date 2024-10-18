<template>
  <div class="container mt-4">
    <h2>Data Sheets</h2>

    <div v-if="datasheets">
      <ul class="list-group">
        <li
            v-for="datasheet in datasheets"
            :key="datasheet.id"
            @click="goToDataSheet(datasheet.id)"
            class="list-group-item list-group-item-action"
            style="cursor: pointer;"
        >
          {{ datasheet.name }}
        </li>
      </ul>
      <div class="mt-4">
        <button @click="createDataSheet" class="btn btn-success">Create Data Sheet</button>
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

export default defineComponent({
  data() {
    return {
      datasheets: null as { id: number, name: string }[] | null
    };
  },
  async created() {
    try {
      const response = await axios.get('/api/datasheet/for-menu');
      this.datasheets = response.data.data;
    } catch (error) {
      console.error('Error loading data sheets:', error);
    }
  },
  methods: {
    async goToDataSheet(id: number) {
      await router.push({ name: 'DataSheetPage', params: { id } });
    },

    async createDataSheet() {
      await router.push({ name: 'CreateDataSheetPage' });
    }
  }
});
</script>
