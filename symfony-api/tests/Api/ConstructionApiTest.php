<?php

namespace App\Tests\Api;

use Symfony\Bundle\FrameworkBundle\Test\WebTestCase;

class ConstructionApiTest extends WebTestCase
{
    public function testGetProject()
    {
        $client = static::createClient();

        $id = 1000003;

        // Make a GET request to the API endpoint
        $client->request('GET', '/api/constructions/' . $id); 
        
        // Assert that the response status code is 200 OK
        $this->assertEquals(200, $client->getResponse()->getStatusCode());

        // Assert that the response is in JSON format
        $this->assertJson($client->getResponse()->getContent());

        // Optionally, assert that the response contains expected data
        $data = json_decode($client->getResponse()->getContent(), true);
        $this->assertArrayHasKey('id', $data);
        $this->assertArrayHasKey('name', $data);
        $this->assertEquals($id, $data['id']); // Adjust this value based on your test data
    }

    public function testGetProjects()
    {
        $client = static::createClient();

        // Make a GET request to the API endpoint
        $client->request('GET', '/api/constructions'); 
        
        // Assert that the response status code is 200 OK
        $this->assertEquals(200, $client->getResponse()->getStatusCode());

        // Assert that the response is in JSON format
        $this->assertJson($client->getResponse()->getContent());

        // Optionally, assert that the response contains expected data
        $data = json_decode($client->getResponse()->getContent(), true);
        $this->assertArrayHasKey('hydra:totalItems', $data);
        $this->assertArrayHasKey('hydra:member', $data);
    }
}
