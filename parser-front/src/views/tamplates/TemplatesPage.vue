<template>
  <div class="container mt-4">
    <h2>Templates</h2>
    
    <div v-if="templates">      
      <ul class="list-group">
        <li
            v-for="template in templates"
            :key="template.id"
            @click="goToTemplate(template.id)"
            class="list-group-item list-group-item-action"
            style="cursor: pointer;"
        >
          {{ template.name }}
        </li>
      </ul>
      <div class="mt-4">
        <button @click="createTemplate" class="btn btn-success">Create Template</button>
      </div>
    </div>

    <div v-else>
      <p>Loading templates...</p>
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
      templates: null as { id: number, name: string } [] | null
    };
  },
  async created() {
    try {
      const response = await axios.get('/api/template/for-menu');
      this.templates = response.data.data;
    } catch (error) {
      console.error('Error loading templates:', error);
    }
  },
  methods: {
    async goToTemplate(id: number) {
      await router.push({ name: 'TemplatePage', params: { id } });
    },

    async createTemplate() {
      await router.push({ name: 'CreateTemplatePage' });
    }
  }
});
</script>
