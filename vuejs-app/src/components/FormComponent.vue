<template>
  <v-form ref="form" v-model="valid" @submit.prevent="submitForm">
    <v-text-field 
      v-model="formData.name" 
      :rules="[
        rules.required, 
        () => !!formData.name && formData.name.length <= 200 || 'Name must be less than 200 characters'
      ]" 
      label="Name"
      counter="200"
      required>
    </v-text-field>
    <v-text-field 
      v-model="formData.location" 
      :rules="[
        rules.required, 
        () => !!formData.location && formData.location.length <= 500 || 'Location must be less than 500 characters'
      ]"
      label="Location" 
      required 
      counter="500">
    </v-text-field>
    <v-select 
      v-model="formData.category" 
      :rules="[rules.required]" 
      :items="categoryOptions" 
      label="Category"
      @change="handleCategoryChange">
    </v-select>
    <v-text-field 
      v-if="formData.category === 'Other'" 
      v-model="formData.otherCategory" 
      label="Please specify"
      placeholder="Enter custom category">
    </v-text-field>
    <v-text-field 
      type="date" 
      v-model="formData.startDate" 
      :rules="[rules.required]" 
      label="Start Date" 
      required
      placeholder="YYYY-MM-DD">
    </v-text-field>
    <v-select 
      v-model="formData.stage" 
      :rules="[rules.required]" 
      :items="stageOptions" 
      label="Stage">
    </v-select>
    <v-textarea 
      v-model="formData.details" 
      :rules="[rules.required]" 
      label="Details" 
      required 
      counter 
      maxlength="2000">
    </v-textarea>

    <v-btn color="success" @click="submitForm" :disabled="!valid">Save</v-btn>
    <v-btn color="secondary" @click="goBack">Back</v-btn>
  </v-form>
  <v-snackbar v-model="snackbar.show" :timeout="snackbar.timeout" color="error">
    {{ snackbar.message }}
  </v-snackbar>
</template>


<script>
export default {
  props: {
    formData: {
      type: Object,
      default: () => ({}),
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
      form: {
        name: this.formData.name || '',
        startDate: this.formData.startDate || '',
        stage: this.formData.stage || null
      },
      stageOptions: [
        { value: 1, title: 'Concept' },
        { value: 2, title: 'Design & Documentation' },
        { value: 3, title: 'Pre-Construction' },
        { value: 4, title: 'Construction' },
      ],
      categoryOptions: ['Education', 'Health', 'Office', 'Other'],
      dateFormat: 'yyyy-MM-dd',
      rules: {
        required: value => !!value || 'Required.',
      },
      snackbar: {
        show: false,
        message: '',
        timeout: 6000
      }
    };
  },
  methods: {
    handleStageChange(value) {
      if (value !== 'Other') {
        this.formData.otherStage = ''; // Clear 'Other' field if not 'Other'
      }
    },
    goBack() {
      this.$router.back(); // Navigate back to the previous page
    },
    async submitForm() {
      if (this.$refs.form.validate()) {
        try {
          const payload = {
            ...this.formData,
            startDate: new Date(this.formData.startDate).toISOString(),
            category: this.formData.category === 'Other' ? this.formData.otherCategory : this.formData.category
          };
          await this.onSubmit(payload);
        } catch (error) {
          let errorMessage = 'An error occurred';
          if (error.response && error.response.data && error.response.data.message) {
            errorMessage = error.response.data.message;
          }
          if (error.response && error.response.data && error.response.data.errors) {
            errorMessage = error.response.data.errors;
          }
          this.snackbar.message = errorMessage;
          this.snackbar.show = true;
        }
      }
    }
  }
};
</script>

<style>
.v-text-field__slot {
  width: 100%;
}
</style>
