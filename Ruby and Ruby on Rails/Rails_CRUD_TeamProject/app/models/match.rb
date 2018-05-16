class Match < ApplicationRecord
  belongs_to :team_one, class_name: "Team", foreign_key: "team_one_id"
  belongs_to :team_two, class_name: "Team", foreign_key: "team_two_id"
  validate :date_of_match_in_future?
  validate :different_teams?
  validates :result, :format => {
    :with => /\A([0-9]:[0-9])|([1-9][0-9]*:[1-9][0-9]*)|([0-9]:[1-9][0-9]*)|([1-9][0-9]*:[0-9])\z/,
    :message => " accepts format of 'number:number'"
  }

  private

  def different_teams?
    if(self.team_one != nil && self.team_two != nil)
      if self.team_one.id == self.team_two.id
        errors.add("Team cannot play", "against itself")
      end
    end
  end

  def date_of_match_in_future?
   if self.date > Date.today
     errors.add("Date of match", "cannot be in the future")
   end
  end

end
