<template>
  <v-form ref="form" v-model="valid" @submit.prevent="submitForm">
    <v-text-field v-model="formData.name" :rules="[rules.required]" label="Name" required></v-text-field>
    <v-text-field v-model="formData.location" :rules="[rules.required]" label="Location" required></v-text-field>
    <v-select v-model="formData.category" :rules="[rules.required]" :items="categoryOptions"
      label="Category" @change="handleCategoryChange"></v-select>
    <v-text-field v-if="formData.category === 'Other'" v-model="formData.otherCategory" label="Please specify"
      placeholder="Enter custom category"></v-text-field>
    <v-text-field type="date" v-model="formData.startDate" :rules="[rules.required]" label="Start Date" required placeholder="YYYY-MM-DD"></v-text-field>
    <v-select v-model="formData.stage" :rules="[rules.required]" :items="stageOptions" label="Stage"></v-select>
    <v-text-field v-model="formData.details" :rules="[rules.required]" label="Details" required></v-text-field>
    <v-btn color="success" @click="submitForm">Save</v-btn>
    <v-btn color="secondary" @click="goBack">Back</v-btn>
  </v-form>
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
      menu: false,
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
        required: value => !!value || 'Required.'
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
    submitForm() {
      if (this.$refs.form.validate()) {
        const payload = {
          ...this.formData,
          startDate: new Date(this.formData.startDate).toISOString(),
          category: this.formData.category === 'Other' ? this.formData.otherCategory : this.formData.category 
        };

        this.onSubmit(payload);
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
