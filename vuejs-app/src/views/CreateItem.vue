<template>
    <v-container>
      <v-row justify="center">
        <v-col cols="12" md="8">
          <v-card>
            <v-card-title>Create Item</v-card-title>
            <v-card-text>
              <FormComponent :formData="formData" :onSubmit="createItem" />
            </v-card-text>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </template>
  
  <script>
  import FormComponent from '@/components/FormComponent.vue';
  import { createApiData } from '@/services/dotnetapi'; 
  
  export default {
    components: {
      FormComponent
    },
    data() {
      return {
        formData: {
          name: '',
          location: '',
          category: '',
          startDate: '',
          stage: '',
          details: ''
        }
      };
    },
    methods: {
      async createItem(data) {
        await createApiData(data)
          .then(response => {
            this.$router.push('/');
          })
          .catch(error => {
            throw error;
          });
      }
    }
  };
  </script>
  