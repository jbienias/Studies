class TeamsController < ApplicationController
  before_action :set_team, only: [:show, :edit, :update, :destroy]
  helper_method :sort_column, :sort_direction

  def index
    if params[:search]
      @teams = Team.search(params[:search]).order("created_at DESC")
    else
      @teams = Team.all.order("#{sort_column} #{sort_direction}")
    end
end


  def show
  end

  def new
    @team = Team.new
  end

  def edit
  end

  def create
    @team = Team.new(team_params)

    if @team.save
      redirect_to @team, notice: 'Team was successfully created.'
    else
      render 'new'
    end
  end

  def update
    if @team.update(team_params)
      redirect_to @team, notice: 'Team was successfully updated.'
    else
      render 'edit'
    end
  end

  def destroy
    #wraz z usunieciem druzyny usuwamy jej historie meczy -> kaskadowe usuwanie
    #Match.where(:team_one_id => @team.id).destroy_all
    #Match.where(:team_two_id => @team.id).destroy_all
    @team.destroy
    redirect_to teams_path, notice: 'Team was successfully deleted.'
  end

  private

    def set_team
      @team = Team.find(params[:id])
    end

    def team_params
      params.require(:team).permit(:name, :date_of_founding, :image)
    end

    def sortable_columns
      ["name", "date_of_founding"]
    end

    def sort_column
      sortable_columns.include?(params[:column]) ? params[:column] : "name"
    end

    def sort_direction
      %w[asc desc].include?(params[:direction]) ? params[:direction] : "asc"
    end
end
