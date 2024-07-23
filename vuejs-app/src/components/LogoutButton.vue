<template>
    <v-btn @click="logout" color="error">Logout</v-btn>
  </template>
  
  <script>
  import { mapActions } from 'vuex';
  
  export default {
    methods: {
      ...mapActions(['logout']),
      logout() {
        this.logout();
        this.$router.push('/login');
      }
    }
  };
  </script>
  