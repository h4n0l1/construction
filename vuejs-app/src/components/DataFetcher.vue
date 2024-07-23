<template>
    <v-container>
        <v-row>
            <v-col>
                <v-card>
                    <v-card-title>
                        Construction Project List
                    </v-card-title>
                    <v-data-table :headers="headers1" :items="data1" :items-per-page="5" class="elevation-1">
                        <template v-slot:item="props">
                            <tr>
                                <td>{{ props.item.id }}</td>
                                <td>{{ props.item.name }}</td>
                                <td>{{ props.item.location }}</td>
                                <td>{{ props.item.stage }}</td>
                            </tr>
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
                { text: 'ID', value: 'id' },
                { text: 'Name', value: 'name' },
                { text: 'Location', value: 'location' },
                { text: 'Stage', value: 'stage' },
            ]
        };
    },
    created() {
        getSymfonyApi().then(response => {
            this.data1 = response.data['hydra:member'];
        }).catch(error => {
            console.error("Error fetching data from API:", error);
        });
    }
};
</script>