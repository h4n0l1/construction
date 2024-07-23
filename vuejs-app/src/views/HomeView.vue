<template>
  <Construction />
</template>

<script>
import { defineComponent } from 'vue';

// Components
import Construction from '../components/DataFetcher.vue';

export default defineComponent({
  name: 'HomeView',

  components: {
    Construction,
  },
});
</script>
