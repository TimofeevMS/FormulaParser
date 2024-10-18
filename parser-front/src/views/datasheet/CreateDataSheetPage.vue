<template>
  <div class="container mt-4">
    <h2>Create Data Sheet</h2>

    <div v-if="templates">      
      <div class="mb-3">
        <select
            required
            type="text"
            id="templateName"
            class="form-control"
            v-model="selectedTemplate"
            v-bind:value="selectedTemplate"
            @change="handleTemplateChange(selectedTemplate)"
        >
          <option v-for="template in templates" :key="template.id" :value="template.id">
            {{ template.name }}
          </option>
        </select>
      </div>

      <div  v-show="selectedTemplate" class="mb-3">
        <input
            type="text"
            id="templateName"
            class="form-control"
            v-model="datasheet.name"
            placeholder="Enter data sheet name"
        />
      </div>

      <div v-show="selectedTemplate">
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
                :placeholder="value.type === 4 ? 'Formula' : 'Value'"
                :disabled="value.type === 4"
            />
          </div>
        </div>
  
        <div class="mt-5">
          <button @click="saveTemplate" class="btn btn-success">Save Data Sheet</button>
        </div>
      </div>
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
      selectedTemplate : null as number | null,
      
      datasheet: {} as {
        templateId: number | null,
        name: string,
        values: {
          id: number,
          name: string,
          value: string,
          type: number
        }[]
      },

      templates: null as { id: number, name: string } [] | null,
    };
  },
  async created() {
    try {
      const response = await axios.get('/api/template/for-menu/');
      this.templates = response.data.data;
    } catch (error) {
      console.error('Error fetching templates:', error);
      return null;
    }
  },
  
  methods: {
    async handleTemplateChange(selectedTemplate: any) {
      try {
        const response = await axios.get(`/api/template/for-datasheet/${selectedTemplate}`);
        this.datasheet = response.data.data;
      } catch (error) {
        console.error('Error saving data sheet:', error);
      }
    },

    validateTemplate() {
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

    async saveTemplate() {
      if (!this.validateTemplate()) {
        return;
      }

      try {
        this.datasheet.templateId = this.selectedTemplate;
        const response = await axios.post(`/api/datasheet`, this.datasheet);
        const sheetId = response.data.data;
        await router.push({ name: 'DataSheetPage', params: { id: sheetId } });
      } catch (error) {
        console.error('Error saving data sheet:', error);
      }
    },
  }
});
</script>
