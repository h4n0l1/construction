<template>
    <v-container>
        <v-row justify="center">
            <v-col cols="12" md="4">
                <v-card>
                    <v-card-title>Login</v-card-title>
                    <v-card-text>
                        <v-form @submit.prevent="login">
                            <v-text-field v-model="email" label="Email" required></v-text-field>
                            <v-text-field v-model="password" label="Password" type="password" required></v-text-field>
                            <v-btn type="submit" color="primary">Login</v-btn>
                        </v-form>
                    </v-card-text>
                </v-card>
                <v-snackbar v-model="snackbar" :color="errorColor" top multi-line>
                    {{ errorMessage }}
                    <v-btn color="white" text @click="snackbar = false">Close</v-btn>
                </v-snackbar>
            </v-col>
        </v-row>
    </v-container>
</template>

<script>
import { mapActions } from 'vuex';

export default {
    data() {
        return {
            email: '',
            password: '',
            snackbar: false,
            errorMessage: '',
            emailRules: [
                v => !!v || 'Email is required',
                v => /.+@.+\..+/.test(v) || 'E-mail must be valid'
            ],
            passwordRules: [
                v => !!v || 'Password is required',
                v => (v && v.length >= 6) || 'Password must be at least 6 characters'
            ]
        };
    },
    methods: {
        ...mapActions({ loginAction: 'loginTo' }),
        async login() {
            try {
                await this.loginAction({ email: this.email, password: this.password });
                this.$router.push('/');
            } catch (error) {
                this.snackbar = true;
                this.errorMessage = error.message || 'Login failed. Please try again.';
            }
        }
    }
};
</script>