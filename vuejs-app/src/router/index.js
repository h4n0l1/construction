import Vue from 'vue';
import Router from 'vue-router';
import DataFetcher from '@/components/DataFetcher.vue';
import CreateItem from '@/views/CreateItem.vue';
import EditItem from '@/views/EditItem.vue';

Vue.use(Router);

export default new Router({
  routes: [
    {
      path: '/',
      name: 'DataFetcher',
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
});
