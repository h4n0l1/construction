<template>
    <v-container>
      <v-row justify="center">
        <v-col cols="12" md="8">
          <v-card>
            <v-card-title>Edit Project</v-card-title>
            <v-card-text>
              <FormComponent :formData="formData" :onSubmit="editItem" />
            </v-card-text>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </template>
  
  <script>
  import FormComponent from '@/components/FormComponent.vue';
  import { getApiDataById } from '@/services/symfonyapi';
  import { updateApiData } from '@/services/dotnetapi'; 
  
  export default {
    components: {
      FormComponent
    },
    data() {
      return {
        formData: {
          id: null,
          name: '',
          location: '',
          category: '',
          startDate: '',
          stage: '',
          details: '',
        }
      };
    },
    created() {
      const id = this.$route.params.id;
      getApiDataById(id)
        .then(response => {
          this.formData = response.data;
          this.formData.startDate = response.data.startDate.split('T')[0];
        })
        .catch(error => {
          console.error('Error fetching item:', error);
        });
    },
    methods: {
      async editItem(data) {
        await updateApiData(data.id, data)
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
  