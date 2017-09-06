%% Plot the ANN in 3d ver1 with mdoules ver 2
% Here each module has its own nodes with the same color, the shared
% connecitons are remaked, ...
% also it is used the 3d, where the modules are separated by layers
%
% Author: Carlos Torres and Victor Landassuri
% Created: 25 Jul 2011
% Modified: 

%% Function 
function plotModuleANN3D1(n,coord,stiles,plotModuleBox,noModules,Module,h)




%% check who is missing in the original list and create a new one with the
% new order, this is doe to avoid not to plot some modules that do not have
% hidden nodes

% create a new list with the actual modules
cont = 0;
actualMod(1,1) = 0;
for i=1:noModules
    actualMod(1,i) = Module{1,i}.name;
end

% check how is missing and create a second list
mod2Add = zeros(1,n.noModules);
for i=n.nameMod
    if isempty( find(i==actualMod, 1) )
        % means this value was not found
        cont = cont + 1;
        mod2Add(1,cont) = i;
    end
end


% The new list of modules 
actualMod2 = [actualMod mod2Add(1,1:cont)];
idxMod = 0;


%% rearange the coordenates of each node into 3d
if n.var.considerInputsInMod == n.C.ON
    for i = [n.inputs n.hidden n.outputs]
        idxMod = find(n.nodesInMod(i,2) == actualMod2);
        coord(i,3) = idxMod;
    end
else
     for i = [ n.hidden n.outputs]
        idxMod = find(n.nodesInMod(i,2) == actualMod2);
        if ~isempty(idxMod)
            coord(i,3) = idxMod;
        end
     end
    % here the inputs will have a fixed position all in one coordinate in z
    posZ = size(actualMod2,2)/2;
    for i=n.inputs
        coord(i,3) = posZ;
    end
end

%% Star to plot
clf
%hold all
% hold on
% first print the n.inputs at the bottom per module
if n.var.considerInputsInMod == n.C.OFF
    % standard values for inputs
    for i=1:n.noInputs
        noNode = i;                                     % obtain the node number
        if n.nodes(1,i) == 1
            % print normal n.nodes
            h = plot3(coord(i,3), coord(i,1),coord(i,2),'o','Color', [66 111 66]/255, ...
                'LineWidth',2,'MarkerSize',7,...
                'MarkerEdgeColor',[153 50 204]/255); % Medium Sea Green 66;111;66 fpr standar inputs
            % MarkerEdgeColor = DarkOrchid =  153 50 204/255
             hold on
        else
            % print deactivated node
            h = plot3(coord(i,3), coord(i,1),coord(i,2),'x', ...
                'Color',[0.5412 0.1686 0.8863], 'LineWidth', 2,...
                'MarkerSize',7); %BlueViolet color [138,43,226]/255
             hold on
        end
    end
    
else
    % if they are considered
    
    for m=1:noModules
        for i=1:n.noInputs
            %noNode = i;                                     % obtain the node number
            if n.nodesInMod(i,2) == Module{1,m}.name
                if n.nodes(1,i) == 1
                    % print normal n.nodes
                    h = plot3(coord(i,3), coord(i,1),coord(i,2),stiles.m{1,m}.inp,...
                        'Color', stiles.m{1,m}.color, 'LineWidth',2, ...
                        'MarkerSize',7,...
                        'MarkerFaceColor', stiles.m{1,m}.color, ...
                        'MarkerEdgeColor',[153 50 204]/255);
                    hold on
   %h = circle([coord(i,1),coord(i,2)],n.r,200,stiles.m{1,m}.colorInp,h,noNode);
                else
                    % print deactivated node
                    h = plot3(coord(i,3), coord(i,1),coord(i,2),'o', ...
                        'LineWidth',2,'MarkerSize',7,...
                        'MarkerEdgeColor',[0.5412 0.1686 0.8863]); %BlueViolet color [138,43,226]/255
                    %h = circle([coord(i,1),coord(i,2)],n.r,200,stiles.colorNoInp,h,noNode);
                    hold on
                end
            end
        end
    end
end
% I put separately in case I want to print then in differet color, ...





% print the n.hidden n.nodes
for m=1:noModules
    for i=n.hidden
        if n.nodesInMod(i,2) == Module{1,m}.name
            %noNode = i;
            h = plot3(coord(i,3), coord(i,1),coord(i,2),stiles.m{1,m}.inp,...
                        'Color', stiles.m{1,m}.color, 'LineWidth',2, ...
                        'MarkerSize',stiles.m{1,m}.nodeSize,...
                        'MarkerFaceColor', stiles.m{1,m}.color);
        end
    end
end

% finally print the output(s)
for m=1:noModules
    for i=n.outputs
        if n.nodesInMod(i,2) == Module{1,m}.name
            %noNode = i;
            h = plot3(coord(i,3), coord(i,1),coord(i,2),stiles.m{1,m}.inp,...
                'Color', stiles.m{1,m}.color, 'LineWidth',2, ...
                'MarkerSize',7,...
                'MarkerFaceColor', stiles.m{1,m}.color,...
                'MarkerEdgeColor',[153 50 204]/255);
        end
    end
end
%--------------------------------------------------------------------------

% what happend if a module did not had any nodes, but it can still have
% inputs, with the previous code I made sure that all hidden nodes are
% plotted, now check only inputs and outputs


    
cont2 = 1;  % second index to cont all new modules to add
for m=noModules + 1: noModules + cont % m start there to take the next format
    for i=[n.inputs n.outputs]
        if n.nodesInMod(i,2) == mod2Add(1,cont2) 
            %noNode = i;
             if n.nodes(1,i) == 1
            h = plot3(coord(i,3), coord(i,1),coord(i,2),stiles.m{1,m}.inp,...
                'Color', stiles.m{1,m}.color, 'LineWidth',2, ...
                'MarkerSize',stiles.m{1,m}.nodeSize,...
                'MarkerFaceColor', stiles.m{1,m}.color,...
                'MarkerEdgeColor',[153 50 204]/255);
             else
                  % print deactivated node
                    h = plot3(coord(i,3), coord(i,1),coord(i,2),'o', ...
                        'Color',[0.5412 0.1686 0.8863], ...
                        'LineWidth',2,'MarkerSize',7); 
             end
        end
    end
    cont2 = cont2 + 1;
end
%--------------------------------------------------------------------------


% change the view of the figure to see better the patterns
view(-14.0, 20.0);
% xlabel('x')
% ylabel('y')
% zlabel('z')
hold on
hold all







% then draw the connectins for inputs, hidde and outputs for each module
for i=n.inputs
    
    if n.var.considerInputsInMod == n.C.OFF
        % standard values for inputs
        % if they are considered into the mdularity
        for j = [n.hidden n.outputs]
            if ( (n.nodes(1,j) == 1) && (n.nodes(1,i) == 1) )
                if n.CW(i,j) == 1
                    Line(coord(i,:), coord(j,:), '-', [66 111 66]/255); % z = [1,1] not sure waht it does in 3d
                    %error('Check what this line doe sin d3 plot')
                    
                end
            end
        end
        
        
    else
        % if they are considered into the mdularity
        for j = [n.hidden n.outputs]
            if ( (n.nodes(1,j) == 1) && (n.nodes(1,i) == 1) )
                if n.CW(i,j) == 1
                    % check if both are in the same module (strong connection)
                    if n.nodesInMod(i,2) == n.nodesInMod(j,2)
                        % identify the module and plot with the same format
                        idxMod = find(n.nodesInMod(i,2) == actualMod2);   % obtain the postition of the format of the module
                        if ~isempty(idxMod)
                            Line(coord(i,:), coord(j,:), '-', stiles.m{1,idxMod}.color); % z = [1,1]
                        end
                    else
                        % share connection
                        Line(coord(i,:), coord(j,:), stiles.sharedConn, ...
                            stiles.sharedConnColor);
                    end
                end
            end
        end
    end % end if consider inputs or not into modularity
end


% draw connections n.hidden n.nodes
for i= [n.hidden n.outputs]
    for j = [n.hidden n.outputs]
        if ( (n.nodes(1,j) == 1) && (n.nodes(1,i) == 1) )
            if n.CW(i,j) == 1
                % check if both are in the same module (strong connection)
                if n.nodesInMod(i,2) == n.nodesInMod(j,2)
                    % identify the module and plot with the same format
                    idxMod = find(n.nodesInMod(i,2) == actualMod2);   % obtain the postition of the format of the module
                    if ~isempty(idxMod)
                        Line(coord(i,:), coord(j,:), '-', stiles.m{1,idxMod}.color); % z = [1,1]
                    end
                else
                    % share connection
                    Line(coord(i,:), coord(j,:), stiles.sharedConn, ...
                        stiles.sharedConnColor);
                end
            end
        end
    end
end

% draw connectins for n.bias
for i= [n.hidden n.outputs]
    if n.nodes(1,i) == 1
        if n.bias(i) == 1
            idxMod = find(n.nodesInMod(i,2) == actualMod2);
            if ~isempty(idxMod)
                LineBias(coord(i,:), stiles.m{1,idxMod}.color); % z = [1,1]
            end
        end
    end
end


% %drawLineModule(plotModuleBox, noModules, Module, stiles); if I want to
% %create a function
% 
% % plot the box of the module
% if plotModuleBox == 1 && noModules > 1
%     for i=1:noModules
%         rectangle('Position',[Module{1,i}.drawMod.x,Module{1,i}.drawMod.y, ...
%             Module{1,i}.drawMod.w,Module{1,i}.drawMod.h],...
%             'Curvature',[0.4],...
%             'LineWidth',2,'LineStyle','--', 'EdgeColor', stiles.m{1,i}.color);
%         plotNameMod( Module{1,i}, stiles.m{1,i}.color )
%     end
% end


