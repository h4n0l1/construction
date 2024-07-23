<template>
    <v-form ref="form" v-model="valid" @submit.prevent="submitForm">
      <v-text-field
        v-model="formData.name"
        :rules="[rules.required]"
        label="Name"
        required
      ></v-text-field>
      <v-text-field
        v-model="formData.location"
        :rules="[rules.required]"
        label="Location"
        required
      ></v-text-field>
      <v-btn color="success" @click="submitForm">Save</v-btn>
    </v-form>
  </template>
  
  <script>
  export default {
    props: {
      formData: {
        type: Object,
        required: true
      },
      onSubmit: {
        type: Function,
        required: true
      }
    },
    data() {
      return {
        valid: false,
        rules: {
          required: value => !!value || 'Required.'
        }
      };
    },
    methods: {
      submitForm() {
        if (this.$refs.form.validate()) {
          this.onSubmit(this.formData);
        }
      }
    }
  };
  </script>
  