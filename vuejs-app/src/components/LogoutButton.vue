<template>
    <v-btn @click="logout" color="error">Logout</v-btn>
  </template>
  
  <script>
  import { mapActions } from 'vuex';
  
  export default {
    methods: {
      ...mapActions({logoutAction: 'logoutTo'}),
      logout() {
        this.logoutAction();
        this.$router.push('/login');
      }
    }
  };
  </script>
  