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
            password: ''
        };
    },
    methods: {
        ...mapActions(['login']),
        async login() {
            console.log('Attempting to login...');
            try {
                await this.login({ email: this.email, password: this.password });
                console.log('Login successful');
                this.$router.push('/');
            } catch (error) {
                console.error('Failed to login:', error);
            }
        }
    }
};
</script>