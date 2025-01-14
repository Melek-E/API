pipeline {
    agent any
    triggers {
        pollSCM('H/5 * * * *')
    }
    environment {
        DOCKERHUB_CREDENTIALS = credentials('dockerhub')
        IMAGE_NAME_SERVER = 'melek16/api-server'
        IMAGE_NAME_CLIENT = 'melek16/client2-client'
    }
    stages {
        stage('Checkout') {
            steps {
                script {
                    echo 'DEBUG: Checking out the repository'
                    sh 'git --version' // Confirm Git version
                    sh 'ls -la'        // List files in the workspace root
                }
                git branch: 'master',
                    url: 'git@github.com:Melek-E/API.git',
                    credentialsId: 'Github_ssh'
                script {
                    echo 'DEBUG: After checkout'
                    sh 'ls -la'        // List files again to confirm successful checkout
                }
            }
        }
        stage('Build Server Image') {
            steps {
                dir('API') {
                    script {
                        echo 'DEBUG: Inside API directory'
                        sh 'ls -la' // Verify the contents of the API directory
                        dockerImageServer = docker.build("${IMAGE_NAME_SERVER}")
                    }
                }
            }
        }
        stage('Build Client Image') {
            steps {
                dir('client2') {
                    script {
                        echo 'DEBUG: Inside client2 directory'
                        sh 'ls -la' // Verify the contents of the client2 directory
                        dockerImageClient = docker.build("${IMAGE_NAME_CLIENT}")
                    }
                }
            }
        }
        stage('Scan Server Image') {
            steps {
                script {
                    echo 'DEBUG: Scanning server image'
                    sh """
                        docker run --rm -v /var/run/docker.sock:/var/run/docker.sock \
                        aquasec/trivy:latest image --exit-code 0 --severity LOW,MEDIUM,HIGH,CRITICAL \
                        ${IMAGE_NAME_SERVER}
                    """
                }
            }
        }
        stage('Scan Client Image') {
            steps {
                script {
                    echo 'DEBUG: Scanning client image'
                    sh """
                        docker run --rm -v /var/run/docker.sock:/var/run/docker.sock \
                        aquasec/trivy:latest image --exit-code 0 --severity LOW,MEDIUM,HIGH,CRITICAL \
                        ${IMAGE_NAME_CLIENT}
                    """
                }
            }
        }
        stage('Push Images to Docker Hub') {
            steps {
                script {
                    echo 'DEBUG: Pushing images to Docker Hub'
                    docker.withRegistry('', "${DOCKERHUB_CREDENTIALS}") {
                        dockerImageServer.push()
                        dockerImageClient.push()
                    }
                }
            }
        }
    }
    post {
        always {
            echo 'DEBUG: Pipeline completed'
            sh 'docker images' // List Docker images at the end of the pipeline
        }
        failure {
            echo 'DEBUG: Pipeline failed'
        }
    }
}
