<template>
  <div class="container mt-4">
    <h2>Template Details</h2>

    <div v-if="template">
      <h3>{{ template.name }}</h3>

      <h4 class="mt-4">Attributes</h4>
      <ul class="list-group">
        <li v-for="attribute in template.attributes" :key="attribute.id" class="list-group-item">
          <div class="row">
            <div class="col-md-4">
              <strong>{{ attribute.name }}:</strong>
            </div>
            <div class="col-md-4">
              <em>{{ attribute.description }}</em>
            </div>
            <div class="col-md-4" v-if="attribute.type === 4">
              {{ attribute.formula }}
            </div>
          </div>
        </li>
      </ul>

      <div class="mt-4">
        <button @click="editTemplate" class="btn btn-success">Edit Template</button>
      </div>
    </div>

    <div v-else>
      <p>Loading template...</p>
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
      templateId: useRoute().params.id,
      
      template: null as {
        id: number,
        name: string,
        attributes: {
          id: number,
          name: string,
          description: string,
          type: number,
          formula?: string
        }[]
      } | null
    };
  },
  async created() {
    try {
      const response = await axios.get(`/api/template/${this.templateId}`);
      this.template = response.data.data;
    } catch (error) {
      console.error('Error loading template:', error);
    }
  },
  methods: {
    async editTemplate() {
      await router.push({name: 'EditTemplatePage', params: {id: this.templateId}});
    },
  }
});
</script>
