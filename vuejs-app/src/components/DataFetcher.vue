<template>
    <v-container>
        <v-row>
            <v-col>
                <v-card>
                    <v-card-title>
                        Construction Project List
                    </v-card-title>
                    <v-btn color="primary" @click="createNewItem">Create New Item</v-btn>
                    <v-data-table :headers="headers1" :items="formattedItems" item-key="id" class="elevation-1">
                        <template v-slot:item.actions="{ item }">
                            <v-icon @click="editItem(item.id)" class="mr-2">mdi-pencil</v-icon>
                            <v-icon @click="deleteItem(item.id)">mdi-delete</v-icon>
                        </template>
                    </v-data-table>
                </v-card>
                <v-snackbar v-model="snackbar" :color="errorColor" top multi-line>
                    {{ notifyMessage }}
                </v-snackbar>
            </v-col>
        </v-row>
    </v-container>
</template>

<script>

import { getApiData } from '@/services/symfonyapi';
import { deleteApiData } from '@/services/dotnetapi';

export default {
    data() {
        return {
            data1: [],
            snackbar: false,
            notifyMessage: '',
            headers1: [
                { title: 'Name', key: 'name' },
                { title: 'Stage', key: 'stage' },
                { title: 'Category', key: 'category' },
                { title: 'Start Date', key: 'startDate' },
                { title: 'Actions', key: 'actions', sortable: false }
            ]
        };
    },
    computed: {
        formattedItems() {
            return this.data1.map(item => ({
                ...item,
                startDate: this.formatDate(item.startDate),
                stage: this.formatStage(item.stage)
            }));
        }
    },
    methods: {
        createNewItem() {
            this.$router.push('/create'); // Redirect to create page
        },
        editItem(id) {
            this.$router.push(`/edit/${id}`); // Redirect to edit page
        },
        formatDate(date) {
            return new Intl.DateTimeFormat('en-GB', {
                year: 'numeric',
                month: '2-digit',
                day: '2-digit'
            }).format(new Date(date));
        },
        formatStage(stage) {
            switch (stage) {
                case 1:
                    return 'Concept';
                case 2:
                    return 'Design & Documentation';
                case 3:
                    return 'Pre-Construction';
                case 4:
                    return 'Construction';
                default:
                    return 'Unknown';
            }
        },
        async deleteItem(id) {
            const confirm = window.confirm('Are you sure you want to delete this item?\n\n #' + id);
            if (!confirm) return;

            try {
                deleteApiData(id).then(response => {
                }).catch(error => {
                    console.error("Error fetching data from API:", error);
                });
                this.snackbar = true;
                this.notifyMessage = 'Project #' + id + ' deleted';
                this.fetchItems();
            } catch (error) {
                this.snackbar = true;
                this.notifyMessage = 'Error deleting item:' + error.message;
            }
        },
        async fetchItems() {
            try {
                getApiData().then(response => {
                    this.data1 = response.data['hydra:member'];
                }).catch(error => {
                    console.error("Error fetching data from API:", error);
                });
            } catch (error) {
                this.snackbar = true;
                this.notifyMessage = 'Error fetching item:' + error.message;
            }
        },
    },
    created() {
        this.fetchItems();
    }
};
</script>