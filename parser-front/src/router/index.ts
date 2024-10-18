import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import CreateTemplatePage from '../views/tamplates/CreateTemplatePage.vue'
import TemplatesPage from '../views/tamplates/TemplatesPage.vue'
import TemplatePage  from "../views/tamplates/TemplatePage.vue";
import EditTemplatePage from "../views/tamplates/EditTemplatePage.vue";

import CreateDataSheetPage from "@/views/datasheet/CreateDataSheetPage.vue";
import DataSheetsPage from "../views/datasheet/DataSheetsPage.vue";
import DataSheetPage from "@/views/datasheet/DataSheetPage.vue";
import EditDataSheetPage from "@/views/datasheet/EditDataSheetPage.vue";

const routes: Array<RouteRecordRaw> = [  
  {
    path: '/datasheets/create',
    name: 'CreateDataSheetPage',
    component: CreateDataSheetPage
  },
  {
    path: '/datasheets',
    name: 'DataSheetsPage',
    component: DataSheetsPage
  },
  {
    path: '/datasheet/:id',
    name: 'DataSheetPage',
    component: DataSheetPage
  },
  {
    path: '/datasheet/edit/:id',
    name: 'EditDataSheetPage',
    component: EditDataSheetPage
  },
  {
    path: '/template/create',
    name: 'CreateTemplatePage',
    component: CreateTemplatePage
  },
  {
    path: '/templates',
    name: 'TemplatesPage',
    component: TemplatesPage
  },
  {
    path: '/template/:id',
    name: 'TemplatePage',
    component: TemplatePage
  },
  {
    path: '/template/edit/:id',
    name: 'EditTemplatePage',
    component: EditTemplatePage
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
