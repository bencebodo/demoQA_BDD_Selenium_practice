pipeline {
    agent {
        docker { 
            image 'mcr.microsoft.com/dotnet/sdk:10.0' 
            args '-u root' 
        }
    }

    stages {
        stage('Checkout') {
            steps {
                echo 'Pull repository...'
                checkout scm
            }
        }

        stage('Install Chrome & Dependencies') {
            steps {
                echo 'Installing Google Chrome and Linux dependencies...'
                sh '''
                apt-get update
                apt-get install -y wget gnupg
                wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | apt-key add -
                sh -c 'echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google.list'
                apt-get update
                apt-get install -y google-chrome-stable
                '''
            }
        }

        stage('Restore & Build') {
            steps {
                echo 'Build solution...'
                sh 'dotnet restore'
                sh 'dotnet build --configuration Release'
            }
        }

        stage('UI tests') {
            steps {
                script{
                    updateGitHubStatus('ci/jenkins/ui-tests', 'UI tests started...', 'PENDING')
                }

                catchError(buildResult: 'FAILURE', stageResult: 'FAILURE') {
                    sh 'dotnet test Demoqa_BDD/Demoqa_BDD.csproj --configuration Release --logger "nunit;LogFileName=TestResult.xml" --results-directory ./TestResults'            
                }
            
                script {
                    if (currentBuild.result == 'FAILURE') {
                        updateGitHubStatus('ci/jenkins/ui-tests', 'UI tests failed!', 'FAILURE')
                    } else {
                        updateGitHubStatus('ci/jenkins/ui-tests', 'UI tests passed!', 'SUCCESS')
                    }
                }
            }
        }
    }

    post {
        always {
            echo 'Archiving NUnit test results...'
            
            archiveArtifacts artifacts: 'TestResults/*.xml', allowEmptyArchive: true
            
            nunit testResultsPattern: 'TestResults/*.xml'
        }
        success {
            echo 'All tests passed successfully!'
        }
        failure {
            echo 'Some tests failed. Check the TestResults.xml file.'
        }
    }
}

def updateGitHubStatus(String contextName, String msg, String state) {
    step([
        $class: 'GitHubCommitStatusSetter',
        contextSource: [
            $class: 'ManuallyEnteredCommitContextSource',
            context: contextName
        ],
        statusResultSource: [
            $class: 'ConditionalStatusResultSource', 
            results: [
                [
                    $class: 'AnyBuildResult', 
                    message: msg, 
                    state: state
                ]
            ]
        ]
    ])
}