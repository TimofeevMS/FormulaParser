<template>
  <div class="container mt-4">
    <h2>Create Template</h2>

    <div v-if="template">
      <div class="mb-3">
        <input
            type="text"
            id="templateName"
            class="form-control"
            v-model="template.name"
            placeholder="Enter template name"
        />
      </div>

      <h4>Attributes</h4>
      <div v-for="(attribute, index) in template.attributes" :key="attribute.id" class="mb-3 row">
        <div class="col-md-3">
          <input
              type="text"
              :id="'attr-' + index"
              class="form-control"
              v-model="attribute.name"
              placeholder="Attribute name"
          />
        </div>
        <div class="col-md-3">
          <input
              type="text"
              id="attributeDescription"
              class="form-control"
              v-model="attribute.description"
              placeholder="Attribute description"
          />
        </div>
        <div class="col-md-2">
          <select
              id="attributeType"
              class="form-select"
              v-model="attribute.type"
              @change="handleTypeChange(attribute)"
          >
            <option :value="2">Number</option>
            <option :value="4">Formula</option>
          </select>
        </div>
        <div class="col-md-2">
          <div v-show="attribute.type === 4">
            <input
                type="text"
                class="form-control"
                v-model="attribute.formula"
                placeholder="Enter formula"
            />
          </div>
        </div>
        <div class="col-md-2">
          <button class="btn btn-danger" @click="removeAttribute(index)">Delete</button>
        </div>
      </div>

      <button @click="addAttribute" class="btn btn-primary">Add Attribute</button>

      <div class="mt-5">
        <button @click="saveTemplate" class="btn btn-success">Save Template</button>
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
      template: {} as {
        id: number,
        name: string,
        attributes: {
          id: number,
          name: string,
          description: string,
          type: number,
          formula?: string
        }[]
      }
    };
  },
  methods: {
    addAttribute() {
      if (this.template) {
        if (!this.template.attributes)
          this.template.attributes = [];
        this.template.attributes.push({
          id: Date.now(),
          name: '',
          description: '',
          type: 2,
          formula: ''
        });
      }
    },

    handleTypeChange(attribute: any) {
      if (attribute.type === 4 && !attribute.formula) {
        attribute.formula = '';
      } else if (attribute.type !== 4) {
        attribute.formula = '';
      }
    },

    removeAttribute(index: number) {
      if (this.template) {
        this.template.attributes.splice(index, 1);
      }
    },

    validateTemplate() {
      if (!this.template.name) {
        alert('Template name is required.');
        return false;
      }

      for (const attribute of this.template.attributes) {
        if (!attribute.name || !attribute.description) {
          alert(`All fields for attribute "${attribute.name}" are required.`);
          return false;
        }

        if (attribute.type === 4 && !attribute.formula) {
          alert(`Formula for attribute "${attribute.name}" is required.`);
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
        const response = await axios.post(`/api/template`, this.template);
        const templateId = response.data.data;
        await router.push({ name: 'TemplatePage', params: { id: templateId } });
      } catch (error) {
        console.error('Error saving template:', error);
      }
    },
  }
});
</script>
