require 'rspec'
require 'simplecov'
require 'fakefs/spec_helpers'

SimpleCov.start

require_relative '../lib/user'
require_relative '../lib/game'
require_relative '../lib/to_a'
require_relative '../lib/print'
require_relative '../lib/menu'

RSpec.configure do |config|
  config.before(:all) { silence_output }
  config.after(:all) { enable_output }
  config.mock_with :rspec do |mocks|
  config.include FakeFS::SpecHelpers, fakefs: true
  mocks.verify_doubled_constant_names = true
  end
end

def silence_output
  @orig_stderr = $stderr
  @orig_stdout = $stdout
  $stderr = File.new('spec/output', 'w')
  $stdout = File.new('spec/output', 'w')
end

def enable_output
  $stderr = @orig_stderr
  $stdout = @orig_stdout
  @orig_stderr = nil
  @orig_stdout = nil
end

def game_creator
  Game.new(
    ('a'..'z').to_a.sample(7).join,
    (10..100).to_a.sample(1).join.to_s,
    (1990..2017).to_a.sample(1).join.to_s,
    ('a'..'z').to_a.sample(3).join
  )
end

def user_creator
  User.new(
    ('a'..'z').to_a.sample(7).join,
    ('a'..'z').to_a.sample(7).join,
    rand(Date.civil(1990, 1, 1)..Date.civil(2017, 12, 31)).to_s,
    ('a'..'z').to_a.sample(3).join,
    (10..100).to_a.sample(1).join.to_s
  )
end

def user_to_delete_creator
  User.new(
    ('a'..'z').to_a.sample(7).join,
    ('a'..'z').to_a.sample(7).join,
    rand(Date.civil(1990, 1, 1)..Date.civil(2017, 12, 31)).to_s,
    ('a'..'z').to_a.sample(3).join,
    (0..3).to_a.sample(1).join.to_s
  )
end

def random_name
  ('a'..'z').to_a.sample(10).join.to_s
end
