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
            </v-col>
        </v-row>
    </v-container>
</template>

<script>

import { getApiData } from '@/services/symfonyapi';

export default {
    data() {
        return {
            data1: [],
            headers1: [
                { title: 'Name', key: 'name' },
                { title: 'Location', key: 'location' },
                { title: 'Category', key: 'category' },
                { title: 'Start Date', key: 'startDate' },
                { title: 'Stage', key: 'stage' },
                { title: 'Details', key: 'details' },
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
        }
    },
    created() {
        getApiData().then(response => {
            this.data1 = response.data['hydra:member'];
        }).catch(error => {
            console.error("Error fetching data from API:", error);
        });
    }
};
</script>