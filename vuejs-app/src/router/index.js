import { createRouter, createWebHashHistory } from 'vue-router'
import DataFetcher from '@/components/DataFetcher.vue';
import CreateItem from '@/views/CreateItem.vue';
import EditItem from '@/views/EditItem.vue';

const routes = [
  {
    path: '/',
    name: 'home',
    component: DataFetcher
  },
  {
    path: '/create',
    name: 'CreateItem',
    component: CreateItem
  },
  {
    path: '/edit/:id',
    name: 'EditItem',
    component: EditItem
  }
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router
