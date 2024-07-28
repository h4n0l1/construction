<?php

namespace App\Tests\Controller;

use Symfony\Bundle\FrameworkBundle\Test\WebTestCase;

class HealthCheckControllerTest extends WebTestCase
{
    public function testHealthCheck()
    {
        $client = static::createClient();
        $client->request('GET', '/healthz');

        $this->assertEquals(200, $client->getResponse()->getStatusCode());
        $this->assertJsonStringEqualsJsonString(
            json_encode(['status' => 'ok']),
            $client->getResponse()->getContent()
        );
    }
}
