%% Script to test different ways to initialize the ANNs
% StestRandomExp2.m
%
% Here is the second version to generate ANN randonly with a more elaborate
% way to initialize them.
%
% Here we are not interested in creation of modules, just to initialize
% them in a better way
%
% The same variables are saved as in StestRandomExp1.m ::: number of
% connections, number of nodes isolated, arch modularity and number of
% nodes. And the two versions oen with one output and other with two outputs
%
% To run the first part ofthis exp put minOut and maxOut to 1,
% in the second part put maxOut to n
%
% Here is considered the case where a nodes is isolated form inputs and
% from outputs, the values are saved in separate variables, and ploted
% togeter in plotExp2BOTH
%
% The expeirment 2 is in the next way: First are fill the columns and then
% the lines
% In th exp3, firt are filled the lines and then the columns
%
% Saved::: A,B,...,L expeimnt of exp2 nad exp3
% 100 netowrks are generated to test the results statistically
%
% 
% There is a nother files to test stsatistically and to plot the result in
% the same folder
%
% Here is not taken into account the bias
%
% 
%
% Zones of the matrix to initialize
% I2H       inputs to hidden nodes
% I2O       inputs to Outputs nodes
% H2H
% H2O
% O2O       (in case there are more than one output)
%
% Created:      9 Oct 2007
% Modified at:  9 Oct 2010
% Author Carlos Torres and Victor Landassuri

%%
clear
clc

path1 = pwd;
%% Add paths
cd('..');  cd('LinuxOrWindows');
%use adecuate paht
SLASH = isLinOrWin();
cd('..');
pathExp = pwd;
addpath([pathExp,SLASH,'draw']);
addpath([pathExp,SLASH,'draw',SLASH,'basicShapes']);
addpath([pathExp,SLASH,'ANN']);
addpath([pathExp,SLASH,'testRandomInit',SLASH,'functions']);

cd(path1);


minInp = 1;
maxInp = 50;

minHid = 1;
maxHid = 100;

minOut = 1;

% XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
%% Expeirmnt 2 or 3, 
% 2: the columns are filled first, Filled by Lines
% 3: the lines are fill first, Filled by Columns
%
% NOTE xxxx CHECK THIS XXXX
Exp = 'resExp3';            % options: 'resExp2' or 'resExp3'
fillby = 'h';               % h or v
% XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX


% enter to the dir to save
cd (Exp)

for M = 1:2
    % set up the maximum number of outputs
    if M == 1
        maxOut = 1;
        name2save = Exp';
    else
        minOut = 2;
        maxOut = 50;
        name2save = [Exp,'manyOut'];
        
    end
    
    contInit = 0;
    
    % variables to save the results
    % each line has a value for initiaalization 0.1, 0.2, ..., 1
    % each colum has a repetion of the same values of initialization 1:100
    
    lines = 12;
    cols = 1000;
    
    conn = zeros(lines,cols);
    March = zeros(lines,cols);
    contnoNodes = zeros(lines,cols);
    contnoInputs = zeros(lines,cols);    % number of nodes not conencted to inputs
    noNodes = zeros(lines,cols);
    
    
    %% setup var for the size and others
    for repInit = 1:lines               % Exp A,B,...
        % initDensity = repInit;
        contInit = contInit + 1;
        
        % repeat 100 times to obtain av. std. min and max values later
        for rep = 1:cols
            % clear some variables
            allnodes = 0;
            nodes = 0;
            bias = 0;
            posinputs = 0;
            poshidden = 0;
            hiddenN = 0;
            outputN = 0;
            
            % Create the network randomly
            noInp = double( int16(minInp+ (maxInp - minInp) * rand()) );
            noHid = double( int16(minHid+ (maxHid - minHid) * rand()) );
            noOut = double( int16(minOut+ (maxOut - minOut) * rand()) );
            
            allnodes = noInp+noHid+noOut;
            nodes = ones(1,noInp + noHid + noOut);
            bias = zeros(1,noInp + noHid + noOut);
            
            posinputs = 1:noInp;
            poshidden = noInp+1:noInp+noHid+noOut;
            hiddenN = noInp+1:noInp+noHid;
            outputN = noInp+noHid+1:noInp+noHid+noOut;
            
            % Now create CW
            CW = zeros(allnodes,allnodes);
            
            % Probability matrix
            switch repInit
                % To test a thing (20/11/2010) I will not use IO to see
                % what happend, change it if you want the original Exp 2 o
                % 3
                % Exp C,D, G, H,K and L is the same
                case 1
                    probMat = obtainProbMatExp2A(noInp, noHid, noOut, posinputs, hiddenN, outputN,fillby);
                case 2
                    probMat = obtainProbMatExp2B(noInp, noHid, noOut, posinputs, hiddenN, outputN,fillby);
                case 3
                    probMat = obtainProbMatExp2C(noInp, noHid, noOut, posinputs, hiddenN, outputN,fillby);
                case 4
                    probMat = obtainProbMatExp2D(noInp, noHid, noOut, posinputs, hiddenN, outputN,fillby);
                case 5
                    probMat = obtainProbMatExp2E(noInp, noHid, noOut, posinputs, hiddenN, outputN,fillby);
                case 6
                    probMat = obtainProbMatExp2F(noInp, noHid, noOut, posinputs, hiddenN, outputN,fillby);
                case 7
                    probMat = obtainProbMatExp2G(noInp, noHid, noOut, posinputs, hiddenN, outputN,fillby);
                case 8
                    probMat = obtainProbMatExp2H(noInp, noHid, noOut, posinputs, hiddenN, outputN,fillby);
                case 9
                    probMat = obtainProbMatExp2I(noInp, noHid, noOut, posinputs, hiddenN, outputN,fillby);
                case 10
                    probMat = obtainProbMatExp2J(noInp, noHid, noOut, posinputs, hiddenN, outputN,fillby);
                case 11
                    probMat = obtainProbMatExp2K(noInp, noHid, noOut, posinputs, hiddenN, outputN,fillby);
                case 12
                    probMat = obtainProbMatExp2L(noInp, noHid, noOut, posinputs, hiddenN, outputN,fillby);
                    
            end
            
            % The initialization in this case is divided into different parts to optimiz it
            
            % init the connections
            % for the upper right triangle of the matrix, first part (I2h amd I2O)
            for i = posinputs
                for j = poshidden
                    if (rand() <= probMat(i,j))
                        CW(i,j) = 1;
                    end
                end
            end
            
            % 2d part, H2H and H2O and O2O
            for i = poshidden
                for j = poshidden
                    if (j>i)
                        if (rand() <= probMat(i,j))
                            CW(i,j) = 1;
                        end
                    end
                end
            end
            
            % bias just to include them even if they do not count
            % for i=poshidden
            %     if (rand() <= initDensity)
            %         bias(i) = 1;
            %     end
            % end
            
            %% measure variables to save
            
            % number of connections
            conn(contInit,rep) = countConnections(CW,bias);
            
            
            %% plot the ann just for me, to analize, comment it when run all exp
            % Plot into screen the ANN with my idea of modules (there is a shared module)
%             clf
%             h = plotANNmod(CW, noInp, noHid,noOut, posinputs, hiddenN, ...
%                 outputN, 2.5, nodes, bias, 1);
            
            
            %% check March of husken
            [noModules, nodesInMod, nameMod, March(contInit,rep) ] = isThereModulesHuskenTopDown(CW, CW, noInp, noHid,noOut, ...
                posinputs, hiddenN, outputN, nodes); % introdece CW in the place of W cause is not ised Mweight here
            
            
            % if noModules = 1 I need to use another way to count the nodes that are
            % not connected to any output
            if noModules == 1
                [nodesInMod] = isConnected2Output(nodesInMod, CW,noHid, ...
                    noOut, posinputs, hiddenN, outputN, allnodes);
            end
            
            
            % nodes that do not connect to any output directly or indirectly
            %contnoNodes = 0;
            for i = 1:allnodes - noOut
                if nodesInMod(i,2) == -2
                    contnoNodes(contInit,rep) = contnoNodes(contInit,rep) +1;
                end
            end
            
            %% save the total number of nodes ion the net
            noNodes(contInit,rep) = allnodes;
            
            
            %% isolated nodes from inputs
            
            % Check that the hidden nodes and outputs are connected directly or
            % indirectly to an input, this is the other case that complement
            % the avobe case
            nodesInMod2 = zeros(allnodes,2);
            [nodesInMod2] = isConnected2Input(nodesInMod2, CW,noInp, noHid, ...
                noOut, posinputs, hiddenN, outputN, allnodes);
            
            for i = [hiddenN outputN]
                if nodesInMod2(i,2) == -2
                    contnoInputs(contInit,rep) = contnoInputs(contInit,rep) +1;
                end
            end
            
            
            
            
        end
        
    end
    
    % save the results
    save( name2save, 'conn', 'March', 'contnoNodes', 'noNodes','contnoInputs' );
    
end


% delete paths
rmpath([pathExp,SLASH,'draw']);
rmpath([pathExp,SLASH,'draw',SLASH,'basicShapes']);
rmpath([pathExp,SLASH,'ANN']);
rmpath([pathExp,SLASH,'testRandomInit',SLASH,'functions']);